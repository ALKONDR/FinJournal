/* eslint-disable react/prop-types */

import React from 'react';
import PropTypes from 'prop-types';

class UserPage extends React.Component {
  render() {
    const data = this.props.userInfo;
    return (
      <div className="userPageHeader">
        <div className="userPageBasic">
          <img alt="avatar" className="userAvatar" />
          <div className="userPageInfo">
            <h3 className="userPageUser">
              {data.username}
            </h3>
            <p className="userPageFol">
              Following: {data.following} Followers: {data.followers}
            </p>
          </div>
          <div className="userPageButtons">
            {this.props.children}
          </div>
        </div>
        <p>
          {data.description}
        </p>
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
