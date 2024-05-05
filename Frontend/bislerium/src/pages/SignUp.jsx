import { Button, Form, Input } from "antd";
import axios from 'axios';
import { Link } from "react-router-dom";

const rules = [
  {
    required: true,
    message: "required",
  },
];

const SignUp = () => {
  const [form] = Form.useForm();

  const handleSubmit = async (values) => {
    try {
      const response = await axios.post('http://localhost:5000/api/Authentication/Register', values);
      console.log('User registered:', response.data);
      alert('Registration successful! Please check your email to confirm your account.');
      form.resetFields();
    } catch (error) {
      if (error.response) {
        console.error('Registration error:', error.response.data);
        alert(`Failed to register: ${error.response.data.message}`);
      } else {
        console.error('Error:', error.message);
        alert('Registration failed. Please try again.');
      }
    }
  };

  return (
    <>
      <div className="h-screen flex justify-center items-center">
        <div className="form-container p-5 rounded-sm w-[350px] border-solid border border-primary bg-[#fcfdfd] cursor-pointer shadow-lg hover:shadow-xl transition duration-300">
          <h1 className="text-[30px] my-2">Create an Account</h1>
          <Form layout="vertical" form={form} onFinish={handleSubmit}>
            <Form.Item
              label="Username"
              name="username"
              className="font-semibold"
              rules={rules}
            >
              <Input placeholder="Enter Your Username" />
            </Form.Item>
            <Form.Item
              label="Email"
              name="email"
              className="font-semibold"
              rules={rules}
            >
              <Input type="email" placeholder="Enter Your Email" />
            </Form.Item>
            <Form.Item
              label="Password"
              name="password"
              className="font-semibold"
              rules={rules}
            >
              <Input.Password
                placeholder="Enter Your Password"
                type="password"
              />
            </Form.Item>
            <Button type="primary" htmlType="submit" block>
              Sign Up
            </Button>
          </Form>
          <div className="mt-4 text-center text-base">
            <span>Already have an account? </span>
            <Link to="/login" className="text-primary hover:text-black">
              Log In
            </Link>
          </div>
        </div>
      </div>
    </>
  );
};

export default SignUp;
