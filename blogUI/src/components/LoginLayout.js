/* eslint-disable jsx-a11y/no-static-element-interactions */

import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';
import api from '../utils/api';

@observer
class LoginLayout extends React.Component {
  constructor(props) {
    super(props);

    this.hideLoginLayout = this.hideLoginLayout.bind(this);
    this.handlePasswordChange = this.handlePasswordChange.bind(this);
    this.handleUsernameChange = this.handleUsernameChange.bind(this);
    this.login = this.login.bind(this);
  }

  hideLoginLayout() {
    this.props.loginState.displayLoginLayout = false;
  }

  login(event) {
    event.preventDefault();

    api.login(this.props.loginState.username, this.props.loginState.password)
      .then((userLoggedIn) => {
        this.props.loginState.userLoggedIn = userLoggedIn;
        this.hideLoginLayout();
      })
      .catch((error) => { console.log('bad request :( ', error); });
  }

  handleUsernameChange(event) {
    this.props.loginState.username = event.target.value;
  }

  handlePasswordChange(event) {
    this.props.loginState.password = event.target.value;
  }

  render() {
    return (
      <div
        className="loginLayoutWithBackground"
        style={{ display: (this.props.loginState.displayLoginLayout ? 'inline' : 'none') }}
      >
        <div role="button" className="darkBackground" onClick={this.hideLoginLayout} />
        <div className="loginLayout">
          <form className="loginForm">
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
            <button onClick={this.login}>
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
