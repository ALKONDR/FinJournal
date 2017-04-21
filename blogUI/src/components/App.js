import React from 'react';
import Header from './Header';
import LoginState from './LoginState';
import LoginLayout from './LoginLayout';

class App extends React.Component {
  render() {
    return (
      <div>
        <Header loginState={LoginState} />
        <LoginLayout loginState={LoginState} />
      </div>
    );
  }
}

module.exports = App;
