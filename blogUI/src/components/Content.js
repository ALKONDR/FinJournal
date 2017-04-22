import React from 'react';
import Nav from './Nav';
import Preview from './Preview';

class Content extends React.Component {
  render() {
    const demoPreview = {
      username: 'username',
      date: {
        day: 22,
        month: 'April',
      },
      readingTime: 3,
      caption: 'Some Caption',
      description: 'Description of the article',
      likes: 1488,
      dislikes: 228,
      comments: 42,
    };
    const previews = [demoPreview, demoPreview, demoPreview, demoPreview];
    return (
      <div className="content">
        <Nav />
        {previews.map(preview => <Preview previewData={preview} />)}
      </div>
    );
  }
}

module.exports = Content;
