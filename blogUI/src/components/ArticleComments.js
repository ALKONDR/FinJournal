/* eslint-disable react/forbid-prop-types */

import React from 'react';
import PropTypes from 'prop-types';
import Comment from './Comment';

class ArticleComments extends React.Component {
  render() {
    return (
      <div className="articleComments">
        <h2 className="commentTitle">
          Comments
        </h2>
        <textarea type="text" placeholder="Write your comment..." className="commentInput" />
        <div className="commentsContainer">
          {this.props.comments.map(comment => <Comment comment={comment} />)}
        </div>
      </div>
    );
  }
}

ArticleComments.propTypes = {
  comments: PropTypes.array.isRequired,
};

module.exports = ArticleComments;
