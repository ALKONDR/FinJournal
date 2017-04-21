/* eslint-disable jsx-a11y/no-static-element-interactions */

import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';

@observer
class LoginLayout extends React.Component {
  constructor(props) {
    super(props);

    this.hideLoginLayout = this.hideLoginLayout.bind(this);
  }

  hideLoginLayout() {
    this.props.loginState.displayLoginLayout = false;
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
            <input type="text" placeholder="username" />
            <label htmlFor="password">
              Password
            </label>
            <input type="password" />
            <button>
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
  }).isRequired,
};

module.exports = LoginLayout;
