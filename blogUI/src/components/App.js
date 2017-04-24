import React from 'react';
import Header from './Header';
import LoginState from './LoginState';
import LoginLayout from './LoginLayout';
import Content from './Content';

const Router = require('react-router-dom').BrowserRouter;
const Route = require('react-router-dom').Route;
const Switch = require('react-router-dom').Switch;

class App extends React.Component {
  render() {
    return (
      <Router>
        <div>
          <Header loginState={LoginState} />
          <LoginLayout loginState={LoginState} />

          <Switch>
            <Route exact path="/" component={Content} />
            <Route exact path="/topic/:topic" component={Content} />
          </Switch>
        </div>
      </Router>
    );
  }
}

module.exports = App;
