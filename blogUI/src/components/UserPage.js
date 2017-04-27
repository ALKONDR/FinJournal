import React from 'react';
import PropTypes from 'prop-types';

class UserPage extends React.Component {
  render() {
    return (
      <div>
        <img alt="avatar" className="userAvatar" />
        {JSON.stringify(this.props.userInfo)}
      </div>
    );
  }
}

UserPage.propTypes = {
  userInfo: PropTypes.shape({
    username: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    followers: PropTypes.number.isRequired,
    following: PropTypes.number.isRequired,
  }).isRequired,
};

module.exports = UserPage;
