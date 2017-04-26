/* eslint-disable react/forbid-prop-types, no-console */

import React from 'react';
import PropTypes from 'prop-types';
import { observable } from 'mobx';
import { observer } from 'mobx-react';
import Comment from './Comment';
import api from '../utils/api';

class _ {
  @observable comments = [];
}

@observer
class ArticleComments extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      commentValue: '',
      // comments: props.comments,
    };

    _.comments = this.props.comments;

    this.handleTextareaChange = this.handleTextareaChange.bind(this);
    this.handleKeyUp = this.handleKeyUp.bind(this);
    this.postComment = this.postComment.bind(this);
    this.addCommentInState = this.addCommentInState.bind(this);
  }

  componentDidUpdate() {
    _.comments = this.props.comments;
    console.log(this.props.comments);
  }

  handleTextareaChange(event) {
    this.setState({
      commentValue: event.target.value,
    });
  }

  handleKeyUp(event) {
    if (event.keyCode === 13) {
      this.postComment(this.state.commentValue);
      this.setState({
        commentValue: '',
      });
    }
  }

  addCommentInState(comment) {
    _.comments.push(comment);
    console.log(this.state.commentValue);
  }

  postComment(content) {
    api.postComment(this.props.username, this.props.caption, content)
      .then((response) => {
        if (response.state >= 200 && response.state < 300) {
          this.addCommentInState({
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
                  if (response.state >= 200 && response.state < 300) {
                    this.addCommentInState({
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
          {_.comments.map(comment => <Comment comment={comment} />)}
        </div>
      </div>
    );
  }
}

ArticleComments.propTypes = {
  comments: PropTypes.array.isRequired,
  username: PropTypes.string.isRequired,
  caption: PropTypes.string.isRequired,
};

module.exports = ArticleComments;
