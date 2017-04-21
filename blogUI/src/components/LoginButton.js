import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';

@observer
class LoginButton extends React.Component {
  constructor(props) {
    super(props);

    this.showLoginLayout = this.showLoginLayout.bind(this);
  }

  showLoginLayout() {
    this.props.loginState.displayLoginLayout = true;
  }

  render() {
    return (
      <button className="loginButton" onClick={this.showLoginLayout}>
        Login/Signup
      </button>
    );
  }
}

LoginButton.propTypes = {
  loginState: PropTypes.shape({
    displayLoginLayout: PropTypes.bool.isRequired,
  }).isRequired,
};

module.exports = LoginButton;
