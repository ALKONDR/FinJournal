import React from 'react';
import PropTypes from 'prop-types';
import api from '../utils/api';
import Preview from './Preview';
import UserPage from './UserPage';

class UserPageController extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      userInfo: {
        username: '',
        description: '',
        following: 0,
        followers: 0,
      },
      previews: [],
      loggedInUser: api.loggedInUser === this.props.match.params.username,
    };

    this.fillUserData = this.fillUserData.bind(this);
  }

  componentDidMount() {
    api.getUser(this.props.match.params.username)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.fillUserData(response.data);
        }
      });
  }

  fillUserData(data) {
    this.setState({
      userInfo: {
        username: data.userName,
        description: data.accountDescription,
        followers: data.followers.length,
        following: data.following.length,
      },
      previews: data.stories.map((element) => {
        const preview = {
          username: element.author,
          date: {
            day: Date(element.date).split(' ')[2],
            month: Date(element.date).split(' ')[1],
          },
          readingTime: element.readingTime,
          caption: element.title,
          description: element.description || 'no description provided',
          likes: element.likes.length,
          dislikes: element.dislikes.length,
          comments: element.comments.length,
        };

        return preview;
      }),
    });
  }

  render() {
    return (
      <div className="userPageContainer">
        <UserPage userInfo={this.state.userInfo} />
        <div className="content">
          {this.state.previews.map(preview => <Preview previewData={preview} />)}
        </div>
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
