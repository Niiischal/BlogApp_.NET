import { Button, Form, Input, } from "antd";
import { Link } from "react-router-dom";

const rules = [
  {
    required: true,
    message: "required",
  },
];

const Login = () => {
  return (
    <>
      <div className="h-screen flex justify-center items-center">
        <div className="form-container p-5 rounded-sm w-[350px] border-solid border border-primary bg-[#fcfdfd] cursor-pointer shadow-lg hover:shadow-xl transition duration-300">
          <h1 className="text-[30px] my-2">Login</h1>
          <Form layout="vertical">
            <Form.Item
              label="Username"
              name="username"
              className="font-semibold"
              rules={rules}
            >
              <Input type=" email" placeholder="Enter Your Username"></Input>
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
              ></Input.Password>
            </Form.Item>
            <Button type="primary" htmlType="submit" block>
              Sign In
            </Button>
            <div className="mt-4 text-center text-base">
              <span>Not a Member? </span>
              <Link to="/" className="text-primary hover:text-black">
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
