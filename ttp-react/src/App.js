import React, { Component } from 'react';
import SelectTask from './containers/SelectTask/SelectTask';
import ListTasks from './containers/ListTasks/ListTasks';
import SyncTasks from './containers/SyncTasks/SyncTasks';

import './App.css';

class App extends Component {
  
  constructor (props) {
    super(props);
    this.addItem = this.addItem.bind(this);
    this.state = {todoItems: []};
  }

  addItem(todoItem) {
    console.log("adding item...");
    this.setState({todoItems: todoItem});
  }

  render() {
    return (
      //<FetchTasks /> 
      <div className="mainContent">
        <SelectTask addItem={this.addItem} />
        <ListTasks todoItems={this.state.todoItems} />
        <SyncTasks addItem={this.addItem} />
      </div>
    );
  }  
}

export default App;
