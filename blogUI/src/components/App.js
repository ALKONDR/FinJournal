import React from 'react';
import PropTypes from 'prop-types';
import Header from './Header';

class App extends React.Component {
  render() {
    return (
      <Header />
    );
  }
}

App.propTypes = {
  message: PropTypes.string.isRequired,
};

module.exports = App;
