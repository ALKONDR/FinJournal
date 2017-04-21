import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';
import api from '../utils/api';

@observer
class LoginButton extends React.Component {
  constructor(props) {
    super(props);

    this.showLoginLayout = this.showLoginLayout.bind(this);
    this.logout = this.logout.bind(this);
  }

  showLoginLayout() {
    this.props.loginState.displayLoginLayout = true;
  }

  logout() {
    api.logout();
    this.props.loginState.userLoggedIn = false;
  }

  render() {
    return (
      <button
        className="loginButton"
        onClick={this.props.loginState.userLoggedIn ? this.logout : this.showLoginLayout}
      >
        {this.props.loginState.userLoggedIn ? 'Logout' : 'Login/Signup'}
      </button>
    );
  }
}

LoginButton.propTypes = {
  loginState: PropTypes.shape({
    displayLoginLayout: PropTypes.bool.isRequired,
    userLoggedIn: PropTypes.bool.isRequired,
  }).isRequired,
};

module.exports = LoginButton;
