import React from 'react';
import PropTypes from 'prop-types';

class UserPageController extends React.Component {
  // constructor(props) {
  //   super(props);
  // }

  componentDidMount() {
    console.log(this.props.match.params.username);
  }

  render() {
    return (
      <div className="userPageContainer">
        <h1>
          Username
        </h1>
      </div>
    );
  }
}

UserPageController.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      username: PropTypes.string.isRequired,
    }).isRequired,
  }).isRequired,
};

module.exports = UserPageController;
