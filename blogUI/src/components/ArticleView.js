import React from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import ArticleComments from './ArticleComments';

class ArticleView extends React.Component {
  render() {
    const data = this.props.articleData;
    return (
      <div className="articleContainer">
        <div className="articleAuthorHeader">
          <img alt="author" className="authorAvatar" />
          <div className="authorInfo">
            <div className="usernameAndFollowButton">
              <h3 className="articleAuthor">
                <Link to={`/users/${data.username}`} >
                  {data.username}
                </Link>
              </h3>
              <button className="button followButton">
                Follow
              </button>
            </div>
            <p className="articleAuthorDescription">
              Account description
            </p>
            <div className="previewDateAndReadingTime">
              <p className="previewDate">
                {data.date.day} {data.date.month}
              </p>
              <p className="readingTime">
                {data.readingTime} min read
              </p>
            </div>
          </div>
        </div>
        <h1 className="articleCaption">
          {data.caption}
        </h1>
        <p className="articleDescription">
          {data.description}
        </p>
        <p className="articleContent">
          {data.content}
        </p>
        <ArticleComments
          comments={data.comments}
          username={data.username}
          caption={data.caption}
          onUpdate={this.props.onUpdate}
        />
      </div>
    );
  }
}

ArticleView.propTypes = {
  articleData: PropTypes.shape({
    username: PropTypes.string.isRequired,

    content: PropTypes.string.isRequired,

    date: PropTypes.shape({
      day: PropTypes.string.isRequired,
      month: PropTypes.string.isRequired,
    }).isRequired,

    readingTime: PropTypes.number.isRequired,
    caption: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,

    likes: PropTypes.array.isRequired,
    dislikes: PropTypes.array.isRequired,
    comments: PropTypes.array.isRequired,
  }).isRequired,
  onUpdate: PropTypes.func.isRequired,
};

module.exports = ArticleView;
