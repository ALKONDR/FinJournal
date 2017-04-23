import React from 'react';
// import ReactRouter from 'react-router-dom';
import Header from './Header';
import LoginState from './LoginState';
import LoginLayout from './LoginLayout';
import Content from './Content';

const Router = require('react-router-dom').BrowserRouter;
const Route = require('react-router-dom').Route;

class App extends React.Component {
  render() {
    return (
      <Router>
        <div>
          <Header loginState={LoginState} />
          <LoginLayout loginState={LoginState} />

          <Route exact path="/" component={Content} />
        </div>
      </Router>
    );
  }
}

module.exports = App;
