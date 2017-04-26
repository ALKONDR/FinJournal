/* eslint-disable react/forbid-prop-types */

import React from 'react';
import PropTypes from 'prop-types';
import Comment from './Comment';

class ArticleComments extends React.Component {
  render() {
    return (
      <div className="articleComments">
        {this.props.comments.map(comment => <Comment comment={comment} />)}
      </div>
    );
  }
}

ArticleComments.propTypes = {
  comments: PropTypes.array.isRequired,
};

module.exports = ArticleComments;
