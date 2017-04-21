import React from 'react';
import PropTypes from 'prop-types';
import NameAndLogo from './NameAndLogo';
import Search from './Search';
import HeaderState from './HeaderState';
import LoginButton from './LoginButton';

class Header extends React.Component {
  render() {
    return (
      <div className="header">
        <NameAndLogo />
        <div className="headerLayout">
          <LoginButton loginState={this.props.loginState} />
          <Search headerState={HeaderState} />
        </div>
      </div>
    );
  }
}

Header.propTypes = {
  loginState: PropTypes.shape({
    displayLoginLayout: PropTypes.bool.isRequired,
  }).isRequired,
};

module.exports = Header;
