import React from 'react';
import PropTypes from 'prop-types';

class Comment extends React.Component {
  render() {
    return (
      <div className="comment">
        {JSON.stringify(this.props.comment)}
      </div>
    );
  }
}

Comment.propTypes = {
  comment: PropTypes.shape({
    author: PropTypes.string,
    date: PropTypes.string,
    content: PropTypes.string,
    likes: PropTypes.array,
    dislikes: PropTypes.array,
  }).isRequired,
};

module.exports = Comment;
