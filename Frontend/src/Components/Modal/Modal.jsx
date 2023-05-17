/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import { Modal, Form, Row, Input, DatePicker, Button, Col } from 'antd';
import dayjs from 'dayjs';
import { api } from '../../Services/HttpHandler';

export default function ModalCreateAndUpdateParking({
  open,
  setOpenModal,
  UpdateIdParking,
}) {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const [teste, setTeste] = useState(false);

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

  const getParkingById = async (id) => {
    await api
      .get(`api/Parking?id=${id}`)
      .then(({ data }) => {
        console.log('oi');
        setTeste(true);
        form.setFieldsValue(data);
        console.log('oi');
      })
      .catch((e) => {
        console.log(e);
      });
  };

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

  useEffect(() => {
    if (UpdateIdParking) {
      console.log(UpdateIdParking);
      getParkingById(UpdateIdParking);
    }
  }, []);

  return (
    <Modal
      className="simple-modal"
      open={open}
      footer={false}
      onCancel={() => setOpenModal(false)}
      closable={false}
    >
      {teste && (
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

            {/* <Col span={24}>
            <Form.Item
              label="EntryDate"
              name="entryDate"
              rules={[
                {
                  required: true,
                  message: 'Please input your EntryDate!',
                },
              ]}
            >
              <DatePicker style={{ width: '35%' }} format="YYYY-MM-DD" />
            </Form.Item>
          </Col> */}

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
      )}
    </Modal>
  );
}
