import React, { useState } from 'react';
import { Form, Input, Button, message } from 'antd';
import axios from 'axios';
import { useSearchParams, useNavigate } from 'react-router-dom';

const ForgetPassword = () => {
  const [form] = Form.useForm();
  const [isResetting, setIsResetting] = useState(false);
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  // Check if the URL contains token parameters to determine the mode
  const token = searchParams.get('token');
  const email = searchParams.get('email');

  // When component mounts, check if we should switch to resetting mode
  useState(() => {
    if (token && email) {
      setIsResetting(true);
    }
  }, [token, email]);

  const handleResetRequest = async (values) => {
    try {
      const response = await axios.post('http://localhost:5142/api/Authentication/ForgotPassword', values);
      message.success(response.data.message);
      form.resetFields();
    } catch (error) {
      message.error(error.response.data.message);
    }
  };

  const handlePasswordReset = async (values) => {
    try {
      const response = await axios.post('http://localhost:5142/api/Authentication/ResetPassword', {
        ...values,
        token,
        email
      });
      message.success('Password reset successful!');
      navigate('/login');
    } catch (error) {
      message.error(error.response.data.message);
    }
  };

  return (
    <div style={{ maxWidth: 300, margin: 'auto', paddingTop: 50 }}>
      <h1 className="text-center">{isResetting ? 'Reset Your Password' : 'Request Password Reset'}</h1>
      <Form form={form} onFinish={isResetting ? handlePasswordReset : handleResetRequest}>
        {!isResetting && (
          <Form.Item
            name="email"
            rules={[{ required: true, message: 'Please input your email!', type: 'email' }]}
          >
            <Input placeholder="Email" />
          </Form.Item>
        )}
        {isResetting && (
          <Form.Item
            name="newPassword"
            rules={[{ required: true, message: 'Please input your new password!' }]}
          >
            <Input.Password placeholder="New Password" />
          </Form.Item>
        )}
        <Button type="primary" htmlType="submit" block>
          {isResetting ? 'Reset Password' : 'Send Reset Link'}
        </Button>
      </Form>
    </div>
  );
};

export default ForgetPassword;
