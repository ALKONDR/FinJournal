import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import { Link } from 'react-router-dom';
import NameAndLogo from './NameAndLogo';
import Search from './Search';
import HeaderState from './HeaderState';
import LoginButton from './LoginButton';
import api from '../utils/api';
import LoginState from './LoginState';

@observer
class Header extends React.Component {
  render() {
    return (
      <div className="header">
        <NameAndLogo />
        <div className="headerLayout">
          <LoginButton loginState={this.props.loginState} />
          <Search headerState={HeaderState} />
          {LoginState.userLoggedIn ?
            <Link to={`/users/${api.loggedInUser}`} >
              <img alt="user" className="authorAvatar" />
            </Link> :
            null
          }
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
