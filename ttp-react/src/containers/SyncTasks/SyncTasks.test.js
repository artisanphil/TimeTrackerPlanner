import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import SyncTasks from './SyncTasks';

describe('SyncTasks', () => {
  it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<SyncTasks />, div);
    ReactDOM.unmountComponentAtNode(div);
  });

  test('has a valid snapshot', () => {
    const component = renderer.create(
      <SyncTasks />
    );
    
    const tree = component.toJSON();

    expect(tree).toMatchSnapshot();
  });
});