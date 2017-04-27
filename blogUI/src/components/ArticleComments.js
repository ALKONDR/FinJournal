/* eslint-disable react/forbid-prop-types, no-console */

import React from 'react';
import PropTypes from 'prop-types';
import Comment from './Comment';
import api from '../utils/api';

class ArticleComments extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      commentValue: '',
    };

    this.handleTextareaChange = this.handleTextareaChange.bind(this);
    this.handleKeyUp = this.handleKeyUp.bind(this);
    this.postComment = this.postComment.bind(this);
    this.addCommentInProps = this.addCommentInProps.bind(this);
  }

  handleTextareaChange(event) {
    this.setState({
      commentValue: event.target.value,
    });
  }

  handleKeyUp(event) {
    if (event.keyCode === 13) {
      if (this.state.commentValue.trim().length > 0) {
        this.postComment(this.state.commentValue.trim());
      }
      this.setState({
        commentValue: '',
      });
    }
  }

  addCommentInProps(comment) {
    this.props.comments.push(comment);
    this.props.onUpdate('comments', this.props.comments);
  }

  postComment(content) {
    api.postComment(this.props.username, this.props.caption, content)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.addCommentInProps({
            author: api.loggedInUser,
            date: Date(),
            content,
            likes: [],
            dislikes: [],
          });
        }
      })
      .catch(() => {
        api.refreshToken()
          .then((refreshed) => {
            if (refreshed) {
              api.postComment(this.props.username, this.props.caption, content)
                .then((response) => {
                  if (response.status >= 200 && response.status < 300) {
                    this.addCommentInProps({
                      author: api.loggedInUser,
                      date: Date(),
                      content,
                      likes: [],
                      dislikes: [],
                    });
                  }
                })
                .catch((error) => { console.log(error); });
            }
          })
          .catch((error) => { console.log(error); });
      });
  }

  render() {
    return (
      <div className="articleComments">
        <h2 className="commentTitle">
          Comments
        </h2>
        <textarea
          type="text"
          placeholder="Write your comment..."
          className="commentInput"
          value={this.state.commentValue}
          onChange={this.handleTextareaChange}
          onKeyUp={this.handleKeyUp}
        />
        <div className="commentsContainer">
          {this.props.comments.map(comment => <Comment comment={comment} />)}
        </div>
      </div>
    );
  }
}

ArticleComments.propTypes = {
  comments: PropTypes.array.isRequired,
  username: PropTypes.string.isRequired,
  caption: PropTypes.string.isRequired,
  onUpdate: PropTypes.func.isRequired,
};

module.exports = ArticleComments;
