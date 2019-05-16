import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import Enzyme, { shallow } from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';
import SelectTask from './SelectTask';

Enzyme.configure({ adapter: new Adapter() });

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

  const props = {
    plannedTasks: [
      { planid: 1, catname: 'First Category', taskname: 'First Task', projectname: 'First Project'},
      { planid: 2, catname: 'Second Category', taskname: 'Second Task', projectname: 'Second Project'}
    ]
  };

  it('shows two items in list', () => {
    const element = shallow(
      <SelectTask />
    ).setState(props);

    expect(element.find('option').length).toBe(2);
  });

});