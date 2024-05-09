import { Button, Form, Input, message } from 'antd';
import axios from 'axios';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const ForgetPassword = () => {
  const [form] = Form.useForm();
  const [isResetting, setIsResetting] = useState(false);
  const [email, setEmail] = useState(""); // State to hold the email
  const navigate = useNavigate();

  const handleResetRequest = async (values) => {
    try {
      const response = await axios.post('http://localhost:5142/api/Authentication/ForgotPassword', values);
      message.success(response.data.message);
      setEmail(values.email); // Set the email in state after successful request
      setIsResetting(true); // Switch to reset mode
    } catch (error) {
      message.error(error.response.data.message);
    }
  };

  const handlePasswordReset = async (values) => {
    try {
      // Include the email in the reset password request
      const response = await axios.post('http://localhost:5142/api/Authentication/ResetPassword', {
        email, // Use the email stored in state
        code: values.code,
        newPassword: values.newPassword
      });
      message.success('Password reset successful!');
      navigate('/login');
    } catch (error) {
      message.error(error.response.data.message);
    }
  };

  return (
    <div className="h-screen flex justify-center items-center">
    <div className='form-container p-5 rounded-sm w-[350px] border-solid border border-primary bg-[#fcfdfd] cursor-pointer shadow-lg hover:shadow-xl transition duration-300'>
      <h1 className="text-center">{isResetting ? 'Enter Your Reset Code' : 'Request Password Reset'}</h1>
      <Form layout='vertical' form={form} onFinish={isResetting ? handlePasswordReset : handleResetRequest}>
        {!isResetting ? (
          <Form.Item
            name="email"
            label="Email Address"
            rules={[{ required: true, message: 'Please input your email!', type: 'email' }]}
          >
            <Input placeholder="Enter your email" />
          </Form.Item>
        ) : (
          <>
            <Form.Item
              name="code"
              label="Reset Code"
              rules={[{ required: true, message: 'Please input your reset code!' }]}
            >
              <Input placeholder="Enter reset code" />
            </Form.Item>
            <Form.Item
              name="newPassword"
              label="New Password"
              rules={[{ required: true, message: 'Please input your new password!' }]}
            >
              <Input.Password placeholder="Enter new password" />
            </Form.Item>
          </>
        )}
        <Button type="primary" htmlType="submit" block>
          {isResetting ? 'Reset Password' : 'Send Reset Link'}
        </Button>
      </Form>
    </div>
    </div>
  );
};

export default ForgetPassword;
