import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { List, Button, Input, Form } from 'antd';
import { UpOutlined, DownOutlined } from '@ant-design/icons';

const { TextArea } = Input;

// Blog List Component
const BlogList = ({ onSelectBlog }) => {
  const [blogs, setBlogs] = useState([]);

  useEffect(() => {
    fetchBlogs();
  }, []);

  const fetchBlogs = async () => {
    const response = await axios.get('http://localhost:5142/api/Blog');
    setBlogs(response.data);
  };

  return (
    <div className="space-y-4">
      {blogs.map(blog => (
        <div key={blog.id} className="p-4 shadow-md">
          <h3 className="font-bold">{blog.title}</h3>
          <p>{blog.summary}</p>
          <Button onClick={() => onSelectBlog(blog.id)}>View Details</Button>
        </div>
      ))}
    </div>
  );
};

// Blog Details Component
const BlogDetails = ({ blogId }) => {
  const [blog, setBlog] = useState(null);
  const [comments, setComments] = useState([]);

  useEffect(() => {
    fetchBlogDetails();
    fetchComments();
  }, [blogId]);

  const fetchBlogDetails = async () => {
    const response = await axios.get(`http://localhost:5142/api/Blog/${blogId}`);
    setBlog(response.data);
  };

  const fetchComments = async () => {
    const response = await axios.get(`http://localhost:5142/api/Comment?blogId=${blogId}`);
    setComments(response.data);
  };

  const handleVote = async (commentId, delta) => {
    await axios.put(`http://localhost:5142/api/Comment/${commentId}`, { delta });
    fetchComments();
  };

  return (
    <div>
      {blog && (
        <>
          <h2 className="text-2xl font-bold">{blog.title}</h2>
          <p>{blog.content}</p>
          <List
            itemLayout="horizontal"
            dataSource={comments}
            renderItem={item => (
              <List.Item
                actions={[
                  <a onClick={() => handleVote(item.id, 1)}><UpOutlined /></a>,
                  <a onClick={() => handleVote(item.id, -1)}><DownOutlined /></a>
                ]}
              >
                <List.Item.Meta
                  title={<a>{item.author}</a>}
                  description={item.text}
                />
              </List.Item>
            )}
          />
        </>
      )}
    </div>
  );
};

// App Component
const App = () => {
  const [selectedBlogId, setSelectedBlogId] = useState(null);

  return (
    <div className="container mx-auto px-4">
      {!selectedBlogId ? (
        <BlogList onSelectBlog={setSelectedBlogId} />
      ) : (
        <BlogDetails blogId={selectedBlogId} />
      )}
    </div>
  );
};

export default App;
