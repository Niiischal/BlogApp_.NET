import { Button, Form, Input, message } from "antd";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

const rules = [
  {
    required: true,
    message: "required",
  },
];

const Login = () => {
  const [form] = Form.useForm();
  const navigate = useNavigate();

  const handleSubmit = async (values) => {
    try {
      const response = await axios.post(
        "http://localhost:5142/api/Authentication/Login",
        values
      );
      if (response.status === 200) {
        localStorage.setItem('token', response.data.token); // Store the JWT token
        message.success("Login successful!");
        navigate("/home"); 
      }
    } catch (error) {
      if (error.response) {
        console.error("Login error:", error.response.data);
        message.error(`Failed to Login: ${error.response.data.message}`);
      } else {
        console.error("Error:", error.message);
        message.error("Login failed. Please try again.");
      }
    }
  };

  return (
    <>
      <div className="h-screen flex justify-center items-center">
        <div className="form-container p-5 rounded-sm w-[350px] border-solid border border-primary bg-[#fcfdfd] cursor-pointer shadow-lg hover:shadow-xl transition duration-300">
          <h1 className="text-[30px] my-2">Login</h1>
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
              label="Password"
              name="password"
              className="font-semibold"
              rules={rules}
            >
              <Input.Password placeholder="Enter Your Password" />
            </Form.Item>
            <div className="flex justify-end mb-3">
              <Link to="/forgetpassword" className="text-primary hover:text-black">
                Forgot password?
              </Link>
            </div>
            <Button type="primary" htmlType="submit" block>
              Sign In
            </Button>
            
            <div className="mt-2 text-center text-base">
              <span>Not a Member? </span>
              <Link to="/register" className="text-primary hover:text-black">
                Create Account
              </Link>
            </div>
          </Form>
        </div>
      </div>
    </>
  );
};

export default Login;
