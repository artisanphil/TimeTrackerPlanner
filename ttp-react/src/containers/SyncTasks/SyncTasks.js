import React, { Component } from 'react';

const API = 'https://localhost:5001/api/';

class SyncTasks extends Component {
    constructor(props) {
      super(props);

      this.state = {
        message: ''
      };      
  
      // This binding is necessary to make `this` work in the callback
      this.handleClick = this.handleClick.bind(this);
    }
  
    handleClick() {
        console.log("clicked sync");
        
        //var tasks = {"planid":493,"starttime":1545652127375,"duration":0};
        var tasks = '{"taskDone":' + localStorage.getItem("tasks") + '}';

        console.log(API + 'plannedtasks');
        console.log(tasks);
        let self = this;
        
        fetch(API + 'plannedtasks',
        {
            method: 'POST',
            headers: {
                  'Accept': 'application/json',
                  'Content-Type': 'application/json'
            },
            body: tasks
        }).then(function(response) {
          console.log(response);
          localStorage.removeItem("tasks");
          //var taskItems = [];
          self.props.addItem("");
          self.setState({
            message: 'Data successfully synced!'
          });
        })
        .catch(function(error) {
          console.log(error);
      });   
    }

    render() {
      return (
        <div>
          <button onClick={this.handleClick}>
              Sync 
          </button>
          <div>{ this.state.message }</div>
        </div>
      );
    }
  }

  export default SyncTasks;