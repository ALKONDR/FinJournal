/* eslint-disable no-console, jsx-a11y/no-static-element-interactions */

import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import api from '../utils/api';

class Preview extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      liked: this.props.previewData.liked,
      disliked: this.props.previewData.disliked,
      likeCount: this.props.previewData.likes,
      dislikeCount: this.props.previewData.dislikes,
    };

    this.setState = this.setState.bind(this);
    this.onLike = this.onLike.bind(this);
    this.onDislike = this.onDislike.bind(this);
  }

  onLike() {
    if (this.state.liked) {
      return;
    }

    const dataForLike = this.props.previewData;
    const currentState = this.state;

    api.likeArticle(dataForLike.username, dataForLike.caption, api.loggedInUser)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.setState({
            liked: true,
            disliked: false,
            likeCount: currentState.likeCount += 1,
            dislikeCount: currentState.disliked ?
              currentState.dislikeCount -= 1 : currentState.dislikeCount,
          });
        }
      })
      .catch(() => {
        api.refresh()
          .then((refreshed) => {
            if (refreshed) {
              api.likeArticle(dataForLike.username, dataForLike.caption, api.loggedInUser)
                .then((response) => {
                  if (response.status >= 200 && response.status < 300) {
                    this.setState({
                      liked: true,
                      disliked: false,
                      likeCount: currentState.likeCount += 1,
                      dislikeCount: currentState.disliked ?
                        currentState.dislikeCount -= 1 : currentState.dislikeCount,
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

  onDislike() {
    if (this.state.disliked) {
      return;
    }

    const dataForDislike = this.props.previewData;
    const currentState = this.state;

    api.dislikeArticle(dataForDislike.username, dataForDislike.caption, api.loggedInUser)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.setState({
            liked: false,
            disliked: true,
            dislikeCount: currentState.dislikeCount += 1,
            likelikeCount: currentState.liked ?
              currentState.likeCount -= 1 : currentState.likeCount,
          });
        }
      })
      .catch(() => {
        api.refresh()
          .then((refreshed) => {
            if (refreshed) {
              api.dislikeArticle(dataForDislike.username, dataForDislike.caption, api.loggedInUser)
                .then((response) => {
                  if (response.status >= 200 && response.status < 300) {
                    this.setState({
                      liked: false,
                      disliked: true,
                      dislikeCount: currentState.dislikeCount += 1,
                      likeCount: currentState.liked ?
                        currentState.likeCount -= 1 : currentState.likeCount,
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
      <div className="previewContainer">
        <div className="previewHeader">
          <img alt="avatar" className="previewUserAvatar" />

          <div className="previewInfo">

            <Link to={`/users/${this.props.previewData.username}`}>
              <p className="previewAuthor">
                {this.props.previewData.username}
              </p>
            </Link>

            <div className="previewDateAndReadingTime">
              <p className="previewDate">
                {this.props.previewData.date.day} {this.props.previewData.date.month}
              </p>
              <p className="readingTime">
                {this.props.previewData.readingTime} min read
              </p>
            </div>

          </div>

        </div>

        <div className="previewContent">
          <Link to={`/users/${this.props.previewData.username}/${this.props.previewData.caption}`}>
            <h2 className="previewCaption">
              {this.props.previewData.caption}
            </h2>
          </Link>
          <p className="previewDescription">
            {this.props.previewData.description}
          </p>
          <img alt="preview" className="previewImage" />
        </div>

        <div className="previewFooter">
          <img
            alt="like"
            className={this.state.liked ? 'likedImg' : 'likeImg'}
            onClick={this.onLike}
          />
          <p className="previewNumbs">{this.state.likeCount}</p>
          <img
            alt="dislike"
            className={this.state.disliked ? 'dislikedImg' : 'dislikeImg'}
            onClick={this.onDislike}
          />
          <p className="previewNumbs">{this.state.dislikeCount}</p>
          <img alt="comments" className="commentsImg" />
          <p className="previewNumbs">{this.props.previewData.comments}</p>
        </div>

      </div>
    );
  }
}

Preview.propTypes = {
  previewData: PropTypes.shape({
    username: PropTypes.string.isRequired,

    date: PropTypes.shape({
      day: PropTypes.string.isRequired,
      month: PropTypes.string.isRequired,
    }).isRequired,

    readingTime: PropTypes.number.isRequired,
    caption: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,

    likes: PropTypes.number.isRequired,
    dislikes: PropTypes.number.isRequired,
    comments: PropTypes.number.isRequired,
    liked: PropTypes.bool.isRequired,
    disliked: PropTypes.bool.isRequired,
  }).isRequired,
};

module.exports = Preview;
