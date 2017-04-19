import React from 'react';
import ReactDom from 'react-dom';
import PropTypes from 'prop-types';
import api from './utils/api';

class App extends React.Component {
  render() {
    api.getUsers();
    return (
      <h1>{this.props.message}</h1>
    );
  }
}
App.propTypes = {
  message: PropTypes.string.isRequired,
};

ReactDom.render(<App message="" />, document.getElementById('root'));
