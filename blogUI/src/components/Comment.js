import React from 'react';
import PropTypes from 'prop-types';

class Comment extends React.Component {
  render() {
    return (
      <div className="comment">
        <div className="commentHeader">
          <img alt="author" className="commentAuthorAvatar" />
          <div className="commentInfo">
            <h3 className="commentAuthor">
              {this.props.comment.author}
            </h3>
            <p className="commentDate">
              {Date(this.props.comment.date).split(' ')[1]} {Date(this.props.comment.date).split(' ')[2]}
            </p>
          </div>
        </div>
        <p className="commentContent">
          {this.props.comment.content}
        </p>
        <div className="commentFooter">
          <img alt="like" className="likeImg" />
          <p className="commentNumbs">{this.props.comment.likes.length}</p>
          <img alt="dislike" className="dislikeImg" />
          <p className="commentNumbs">{this.props.comment.dislikes.length}</p>
        </div>
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
