/* eslint-disable jsx-a11y/no-static-element-interactions, no-console */

import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';
import api from '../utils/api';

@observer
class LoginLayout extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      inLogIn: true,
      email: '',
      repPassword: '',
    };

    this.hideLoginLayout = this.hideLoginLayout.bind(this);
    this.handlePasswordChange = this.handlePasswordChange.bind(this);
    this.handleUsernameChange = this.handleUsernameChange.bind(this);
    this.handleEmailChange = this.handleEmailChange.bind(this);
    this.handleRepPasswordChange = this.handleRepPasswordChange.bind(this);
    this.inLogIn = this.inLogIn.bind(this);
    this.inSignUp = this.inSignUp.bind(this);
    this.login = this.login.bind(this);
    this.signup = this.signup.bind(this);
    this.setState = this.setState.bind(this);
  }

  hideLoginLayout() {
    this.props.loginState.displayLoginLayout = false;
  }

  login(event) {
    event.preventDefault();

    api.login(this.props.loginState.username, this.props.loginState.password)
      .then((userLoggedIn) => {
        this.props.loginState.userLoggedIn = userLoggedIn;
        this.props.loginState.username = '';
        this.props.loginState.password = '';
        this.hideLoginLayout();
      })
      .catch((error) => { console.log('bad request :( ', error); });
  }

  signup(event) {
    event.preventDefault();

    api.signup(this.state.email, this.props.loginState.username, this.props.loginState.password)
      .then((signuped) => {
        if (signuped) {
          this.setState({
            inLogIn: true,
            email: '',
            repPassword: '',
          });
        }
      });
  }

  handleUsernameChange(event) {
    this.props.loginState.username = event.target.value;
  }

  handlePasswordChange(event) {
    this.props.loginState.password = event.target.value;
  }

  handleEmailChange(event) {
    this.setState({
      email: event.target.value,
    });
  }

  handleRepPasswordChange(event) {
    this.setState({
      repPassword: event.target.value,
    });
  }

  inLogIn() {
    this.setState({
      inLogIn: true,
    });
  }

  inSignUp() {
    this.setState({
      inLogIn: false,
    });
  }

  render() {
    return (
      <div
        className="loginLayoutWithBackground"
        style={{ display: (this.props.loginState.displayLoginLayout ? 'inline' : 'none') }}
      >
        <div role="button" className="darkBackground" onClick={this.hideLoginLayout} />
        <div className="loginLayout">
          <div className="loginNavContainer">
            <div className={this.state.inLogIn ? 'logInActive' : 'logIn'} onClick={this.inLogIn} >
              LogIn
            </div>
            <div className={this.state.inLogIn ? 'signUp' : 'signUpActive'} onClick={this.inSignUp} >
              SignUp
            </div>
          </div>
          <form className="loginForm">
            {!this.state.inLogIn ?
              <label htmlFor="email">
                Email
              </label> :
              null
            }
            {!this.state.inLogIn ?
              <input
                type="text"
                placeholder="email"
                value={this.state.email}
                onChange={this.handleEmailChange}
              /> :
              null
            }
            <label htmlFor="username">
              Username
            </label>
            <input
              type="text"
              placeholder="username"
              value={this.props.loginState.username}
              onChange={this.handleUsernameChange}
            />
            <label htmlFor="password">
              Password
            </label>
            <input
              type="password"
              placeholder="password"
              value={this.props.loginState.password}
              onChange={this.handlePasswordChange}
            />
            {!this.state.inLogIn ?
              <label htmlFor="repeatPassword">
                Repeat password
              </label> :
              null
            }
            {!this.state.inLogIn ?
              <input
                type="password"
                placeholder="one more time"
                value={this.state.repPassword}
                onChange={this.handleRepPasswordChange}
              /> :
              null
            }
            <button onClick={this.state.inLogIn ? this.login : this.signup}>
              Submit
            </button>
          </form>
        </div>
      </div>
    );
  }
}

LoginLayout.propTypes = {
  loginState: PropTypes.shape({
    displayLoginLayout: PropTypes.bool.isRequired,
    username: PropTypes.string.isRequired,
    password: PropTypes.string.isRequired,
    userLoggedIn: PropTypes.bool.isRequired,
  }).isRequired,
};

module.exports = LoginLayout;
