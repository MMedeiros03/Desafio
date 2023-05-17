/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
/* eslint-disable import/no-extraneous-dependencies */
import React, { useState } from 'react';
import {
  Modal,
  Form,
  Row,
  Input,
  DatePicker,
  TimePicker,
  Button,
  Col,
} from 'antd';
import { api } from '../../Services/HttpHandler';

export default function ModalCreateAndUpdateParking({
  open,
  setOpenModal,
  UpdateParking,
}) {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();

  const deleteParking = async (id) => {
    await api
      .delete(`api/Parking?id=${id}`)
      .then(({ data }) => {
        console.log(data);
      })
      .catch((e) => {
        console.log(e);
      });
  };
  console.log(UpdateParking);
  form.setFieldsValue(UpdateParking);

  const onFinish = async (values) => {
    setLoading(true);
    const objectCreateParking = {
      entryDate: values.entryDate,
      licensePlate: values.licensePlate,
      model: values.model,
      brand: values.brand,
      color: values.color,
    };
    await api
      .post('api/Parking', objectCreateParking)
      .then(({ data }) => {
        console.log(data);
        setLoading(false);
      })
      .catch((e) => console.log(e));
  };

  return (
    <Modal
      className="simple-modal"
      open={open}
      footer={false}
      onCancel={() => setOpenModal(false)}
      closable={false}
    >
      <Form
        form={form}
        disabled={loading}
        onFinish={onFinish}
        layout="vertical"
      >
        <Row gutter={[24]}>
          <Col span={24}>
            <Form.Item
              label="LicensePlate"
              name="licensePlate"
              rules={[
                {
                  required: true,
                  message: 'Please input your LicensePlate!',
                },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>

          <Col span={24} style={{ display: 'flex', alignItems: 'center' }}>
            <Form.Item
              style={{ display: 'flex', alignItems: 'center' }}
              label="EntryDate"
              name="entryDate"
              rules={[
                {
                  required: true,
                  message: 'Please input your EntryDate!',
                },
              ]}
            >
              <DatePicker style={{ width: '50%' }} />
              <TimePicker style={{ width: '50%' }} />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Model"
              name="model"
              rules={[
                {
                  required: true,
                  message: 'Please input your Model!',
                },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Brand"
              name="brand"
              rules={[
                {
                  required: true,
                  message: 'Please input your Brand!',
                },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Color"
              name="color"
              rules={[
                {
                  required: true,
                  message: 'Please input your Color!',
                },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>
        </Row>
        <Button type="primary" htmlType="submit">
          Send
        </Button>
      </Form>
    </Modal>
  );
}
