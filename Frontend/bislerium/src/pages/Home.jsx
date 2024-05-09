import React from 'react';
import Navbar from "../components/Navbar";
import { Button, Card } from 'antd';
import { UpOutlined, CommentOutlined } from '@ant-design/icons';

function Home() {
  // Sample data to mimic posts
  const posts = [
    {
      id: 1,
      title: "First Post",
      content: "This is the first post content",
      upvotes: 15,
      comments: 3
    },
    {
      id: 2,
      title: "Second Post",
      content: "This is the second post content",
      upvotes: 25,
      comments: 5
    },
    {
      id: 3,
      title: "Third Post",
      content: "This is the third post content",
      upvotes: 8,
      comments: 1
    },
  ];

  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />
      <div className="container mx-auto px-4">
        {posts.map((post) => (
          <Card 
            key={post.id} 
            title={post.title} 
            bordered={false} 
            className="mb-4"
            actions={[
              <Button icon={<UpOutlined />} size="large">{post.upvotes}</Button>,
              <Button icon={<CommentOutlined />} size="large">{post.comments}</Button>
            ]}
          >
            <p>{post.content}</p>
          </Card>
        ))}
      </div>
    </div>
  );
}

export default Home;
