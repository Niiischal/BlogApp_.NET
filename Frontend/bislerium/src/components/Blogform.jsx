import React from "react";
import { Modal, Row, Col, Input, Form, Upload, Button, message } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import axios from "axios";

function Blogform({ showBlogForm, setShowBlogForm }) {
  const [form] = Form.useForm();

  const handleFileChange = (info) => {
    // Assuming single file upload; adjust as needed for multiple files
    if (info.fileList.length > 0) {
      form.setFieldsValue({
        image: info.fileList[0].originFileObj,
      });
    }
  };

  const onFinish = async (values) => {
    try {
      const response = await axios.post(
        "http://localhost:5142/api/Blog",
        values
      );
      if (response.status === 201) {
        message.success("Blog posted successfully!");
        form.resetFields();
        setShowBlogForm(false);
      }
    } catch (error) {
      if (error.response) {
        message.error(`Failed to Post Blog: ${error.response.data.message}`);
      } else {
        message.error("Blog post failed. Please try again.");
      }
    }
  };

  return (
    <div>
      <Modal
        visible={showBlogForm}
        onCancel={() => setShowBlogForm(false)}
        centred
        width={"60%"}
        okText='Post Blog'
      >
        <h1 className='text-xl font-semibold uppercase text-center'>
          Add Blog
        </h1>
        <Form layout='vertical' onFinish={onFinish}>
          <Row>
            <Col span={24}>
              <Form.Item
                label='Title'
                name='title'
                rules={[
                  {
                    required: true,
                    message: "Please provide the title of the blog",
                  },
                ]}
              >
                <Input type='text' />
              </Form.Item>
            </Col>
            <Col span={24}>
              <Form.Item
                label='Content'
                name='content'
                rules={[
                  {
                    required: true,
                    message: "Please provide the content of the blog",
                  },
                ]}
              >
                <Input.TextArea rows={4} />
              </Form.Item>
            </Col>
            <Col span={24}>
              <Form.Item
                label='Image'
                name='imageURL'
                rules={[
                  {
                    required: true,
                    message: "Please provide an image for the blog",
                  },
                ]}
              >
                <Upload
                  beforeUpload={() => false} // Prevents Upload component from uploading the file automatically
                  onChange={handleFileChange}
                  listType='picture'
                >
                  <Button icon={<UploadOutlined />}>Upload</Button>
                </Upload>
              </Form.Item>
            </Col>
          </Row>
        </Form>
      </Modal>
    </div>
  );
}

export default Blogform;
