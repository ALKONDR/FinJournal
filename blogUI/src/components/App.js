import React from 'react';
// import ReactRouter from 'react-router-dom';
import Header from './Header';
import LoginState from './LoginState';
import LoginLayout from './LoginLayout';
import Content from './Content';

// const Router = ReactRouter.BrowerRouter;
// const Route = ReactRouter.Route;

class App extends React.Component {
  render() {
    return (
      <div>
        <Header loginState={LoginState} />
        <LoginLayout loginState={LoginState} />

        <Content />
      </div>
    );
  }
}

module.exports = App;
