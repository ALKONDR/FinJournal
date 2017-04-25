import React from 'react';
import PropTypes from 'prop-types';

class ArticleView extends React.Component {
  render() {
    return (
      <div className="articleContainer">
        <h2 className="articleCaption">
          {this.props.articleData.caption}
        </h2>
        <p className="articleDescription">
          {this.props.articleData.description}
        </p>
      </div>
    );
  }
}

ArticleView.propTypes = {
  articleData: PropTypes.shape({
    username: PropTypes.string.isRequired,

    date: PropTypes.shape({
      day: PropTypes.number.isRequired,
      month: PropTypes.string.isRequired,
    }).isRequired,

    readingTime: PropTypes.number.isRequired,
    caption: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,

    likes: PropTypes.array.isRequired,
    dislikes: PropTypes.array.isRequired,
    comments: PropTypes.array.isRequired,
  }).isRequired,
};

module.exports = ArticleView;
