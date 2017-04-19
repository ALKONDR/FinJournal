import React from 'react';
import PropTypes from 'prop-types';
import api from '../utils/api';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { message: this.props.message };
    api.login('username', 'youwillnotpass');
  }

  componentDidMount() {
    api.getUsers().then((response) => {
      const user = response.data[0].userName;
      this.setState({ message: user });
    });
  }

  render() {
    return (
      <h1>{this.state.message}</h1>
    );
  }
}

App.propTypes = {
  message: PropTypes.string.isRequired,
};

module.exports = App;
