import React from "react";
import { Modal, Row, Col, Input, Form, Upload, Button } from "antd";
import { UploadOutlined } from "@ant-design/icons";

function Blogform({ showBlogForm, setShowBlogForm }) {
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
        <Form layout='vertical'>
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
                name='image'
                rules={[
                  {
                    required: true,
                    message: "Please provide an image for the blog",
                  },
                ]}
              >
                <Upload>
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
