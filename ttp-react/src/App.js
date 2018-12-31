import React, { Component } from 'react';
import SelectTask from './containers/SelectTask/SelectTask';
import ListTasks from './containers/ListTasks/ListTasks';
import SyncTasks from './containers/SyncTasks/SyncTasks';

import './App.css';

class App extends Component {
  

  render() {
    return (
      //<FetchTasks /> 
      <div className="mainContent">
        <SelectTask />
        <ListTasks />
        <SyncTasks />
      </div>
    );
  }  
}

export default App;
