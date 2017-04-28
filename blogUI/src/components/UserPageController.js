/* eslint-disable no-console, eqeqeq */

import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react';
import api from '../utils/api';
import Preview from './Preview';
import UserPage from './UserPage';
import LoginState from './LoginState';

@observer
class UserPageController extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      userInfo: {
        username: '',
        description: '',
        following: 0,
        followers: 0,
        alreadyFollowing: false,
      },
      previews: [],
      loggedInUser: api.loggedInUser === this.props.match.params.username,
    };

    this.fillUserData = this.fillUserData.bind(this);
    this.setState = this.setState.bind(this);
    this.follow = this.follow.bind(this);
    this.unfollow = this.unfollow.bind(this);
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
        alreadyFollowing: data.followers.some(follower => api.loggedInUser === follower),
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
          liked: element.likes.some(like => like.author === api.loggedInUser),
          disliked: element.dislikes.some(dislike => dislike.author === api.loggedInUser),
        };

        return preview;
      }),
    });
  }

  follow(e) {
    e.preventDefault();

    const data = {
      follower: api.loggedInUser,
      following: this.state.userInfo.username,
      currentState: this.state,
    };

    api.follow(data.follower, data.following)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.setState({
            userInfo: {
              username: data.currentState.userInfo.username,
              following: data.currentState.userInfo.following,
              description: data.currentState.userInfo.description,
              followers: data.currentState.userInfo.followers += 1,
              alreadyFollowing: true,
            },
          });
        }
      })
      .catch(() => {
        api.refreshToken()
          .then((refreshed) => {
            if (refreshed) {
              api.follow(data.follower, data.following)
              .then((response) => {
                if (response.status >= 200 && response.status < 300) {
                  this.setState({
                    userInfo: {
                      username: data.currentState.userInfo.username,
                      following: data.currentState.userInfo.following,
                      description: data.currentState.userInfo.description,
                      followers: data.currentState.userInfo.followers += 1,
                      alreadyFollowing: true,
                    },
                  });
                }
              })
              .catch((error) => { console.log(error); });
            }
          })
          .catch((error) => { console.log(error); });
      })
      .catch((error) => { console.log(error); });
  }

  unfollow(e) {
    e.preventDefault();

    const data = {
      follower: api.loggedInUser,
      following: this.state.userInfo.username,
      currentState: this.state,
    };

    api.unfollow(data.follower, data.following)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.setState({
            userInfo: {
              username: data.currentState.userInfo.username,
              following: data.currentState.userInfo.following,
              description: data.currentState.userInfo.description,
              followers: data.currentState.userInfo.followers -= 1,
              alreadyFollowing: false,
            },
          });
        }
      })
      .catch(() => {
        api.refreshToken()
          .then((refreshed) => {
            if (refreshed) {
              api.unfollow(data.follower, data.following)
              .then((response) => {
                if (response.status >= 200 && response.status < 300) {
                  this.setState({
                    userInfo: {
                      username: data.currentState.userInfo.username,
                      following: data.currentState.userInfo.following,
                      description: data.currentState.userInfo.description,
                      followers: data.currentState.userInfo.followers -= 1,
                      alreadyFollowing: false,
                    },
                  });
                }
              })
              .catch((error) => { console.log(error); });
            }
          })
          .catch((error) => { console.log(error); });
      })
      .catch((error) => { console.log(error); });
  }

  render() {
    return (
      <div className="userPageContainer">
        <UserPage userInfo={this.state.userInfo}>
          {this.state.loggedInUser && LoginState.userLoggedIn ?
            <div className="editAndWrite">
              <button>
                Edit
              </button>
              <Link to="/write">
                <button>
                  Write an article
                </button>
              </Link>
            </div>
            :
            <button onClick={this.state.userInfo.alreadyFollowing ? this.unfollow : this.follow} >
              {this.state.userInfo.alreadyFollowing ? 'Unfollow' : 'Follow'}
            </button>
          }
        </UserPage>
        <div className="userPageContent">
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
