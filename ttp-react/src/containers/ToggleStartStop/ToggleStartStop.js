import React, { Component } from 'react';
import './ToggleStartStop.css';

class ToggleStartStop extends Component {
    constructor(props) {
      super(props);
      this.state = {isToggleOn: true};
  
      // This binding is necessary to make `this` work in the callback
      this.handleClick = this.handleClick.bind(this);
    }
  
    handleClick() {
      console.log(this.props.selectedTask);
      this.setState(prevState => ({
        isToggleOn: !prevState.isToggleOn
      }));
      if(this.state.isToggleOn)
      {
        this.recordStarttime(this.props.selectedTask);
      }
      else
      {
        this.recordDuration();
      }
    }

    recordStarttime(planid)
    {
      const now = new Date();
      localStorage.setItem('starttime', now.getTime());
      var taskItems = [];
      if(localStorage.getItem("tasks"))
      {
        taskItems = JSON.parse(localStorage.getItem("tasks"));
      }
      taskItems.push({"planid": planid, "starttime": now.getTime(), "duration": 0});
      localStorage.setItem('tasks', JSON.stringify(taskItems));
    }
  
    recordDuration()
    {
      var taskItems = [];
      if(localStorage.getItem("tasks"))
      {
        taskItems = JSON.parse(localStorage.getItem("tasks"));
      }
      const now = new Date();
      var diffMs = now.getTime() - localStorage.getItem('starttime');
      var duration = Math.round(((diffMs % 86400000) % 3600000) / 60000);
      taskItems[(taskItems.length - 1)]["duration"] = duration;
      localStorage.setItem('tasks', JSON.stringify(taskItems));      
    }
    render() {
      return (
        <button onClick={this.handleClick} className="StartStopButton">
          {this.state.isToggleOn ? 'Start' : 'Stop'}
        </button>
      );
    }
  }

  export default ToggleStartStop;