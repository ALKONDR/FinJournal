import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';

class Preview extends React.Component {
  render() {
    return (
      <div className="previewContainer">
        <div className="previewHeader">
          <img alt="avatar" className="previewUserAvatar" />

          <div className="previewInfo">

            <p className="previewAuthor">
              {this.props.previewData.username}
            </p>

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
          <img alt="like" className="likeImg" />
          <p className="previewNumbs">{this.props.previewData.likes}</p>
          <img alt="dislike" className="dislikeImg" />
          <p className="previewNumbs">{this.props.previewData.dislikes}</p>
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
  }).isRequired,
};

module.exports = Preview;
