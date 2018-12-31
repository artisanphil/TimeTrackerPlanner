import React, { Component } from 'react';
import ToggleStartStop from '../ToggleStartStop/ToggleStartStop';
import './SelectTask.css';


//https://www.robinwieruch.de/react-fetching-data/
//https://blog.hellojs.org/fetching-api-data-with-react-js-460fe8bbf8f2
const API = 'https://localhost:5001/api/';

class SelectTask extends Component {

    constructor(props) {
        super(props);
    
        this.state = {
          plannedTasks: [],
          selectedTask: 0
        };
      }
    
    componentDidMount() {
        if(localStorage.getItem('plannedTasks') != null)
        {
            var data = JSON.parse(localStorage.getItem('plannedTasks'));
            this.setState({ plannedTasks: data, selectedTask: data[0]['planid'] });
        }
        else
        {
            this.fetchTasks();
        }
    }  

    fetchTasks()
    {
        fetch(API + 'plannedtasks')
          .then(response => response.json())
          .then(data => this.savePlanedTasks(data));
    }  
    savePlanedTasks(data) {
        localStorage.setItem('plannedTasks', JSON.stringify(data));
        this.setState({ plannedTasks: data, selectedTask: data[0]['planid'] });
    }  
    handleSelectChange = (event) => {
        this.setState({
            selectedTask: event.target.value
        });
    }      
    
    render() {
        const { plannedTasks } = this.state;

        return (
            <div>
                <select onChange={this.handleSelectChange}>
                {plannedTasks.map(task =>
                    <option value={task.planid} key={task.planid}>
                    {task.catname}: {task.taskname} ({task.projectname})
                    </option>
                )}
                </select>
                <ToggleStartStop selectedTask={this.state.selectedTask} />
            </div>
        );    
    }
}

export default SelectTask;