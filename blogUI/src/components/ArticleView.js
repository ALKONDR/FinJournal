import React from 'react';
import PropTypes from 'prop-types';

class ArticleView extends React.Component {
  componentWillReceiveProps() {
    console.log('article data have to be rendered here');
    console.log(this.props.articleData);
  }
  render() {
    return (
      <div className="articleContainer">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <h2 className="articleCaption">
          {JSON.stringify(this.props)}
        </h2>
        <p className="articleDescription">
          {'description'}
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
