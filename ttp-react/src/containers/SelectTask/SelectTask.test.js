import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import SelectTask from './SelectTask';

describe('SelectTask', () => {
  it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<SelectTask />, div);
    ReactDOM.unmountComponentAtNode(div);
  });

  test('has a valid snapshot', () => {
    const component = renderer.create(
      <SelectTask />
    );
    
    const tree = component.toJSON();

    expect(tree).toMatchSnapshot();
  });
});