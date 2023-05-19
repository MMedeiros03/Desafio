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
  message,
  Button,
  Col,
} from 'antd';
import locale from 'antd/es/date-picker/locale/pt_BR';

import { api } from '../../Services/HttpHandler';

export default function ModalCreateAndUpdateParking({
  open,
  setOpenModal,
  setUpdateTable,
  UpdateTable,
  UpdateParking = false,
}) {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();

  const deleteParking = async () => {
    const { id } = UpdateParking;
    await api
      .delete(`api/Parking?id=${id}`)
      .then(({ data }) => {
        setUpdateTable(!UpdateTable);
        setOpenModal(false);
      })
      .catch((e) => {
        console.log(e);
      });
  };

  form.setFieldsValue(UpdateParking);

  const onFinish = async (values) => {
    setLoading(true);
    const objectCreateParking = {
      entryDate: values.entryDate.subtract(3, 'hours'),
      licensePlate: values.licensePlate,
      model: values.model,
      brand: values.brand,
      color: values.color,
    };
    await api
      .post('api/Parking', objectCreateParking)
      .then(() => {
        setUpdateTable(!UpdateTable);
        form.resetFields();
        setLoading(false);
        setOpenModal(false);
        message.success('Criado com sucesso!');
      })
      .catch(({ response }) => {
        message.error(response?.data?.Message);
        setLoading(false);
      });
  };

  const onUpdateParking = async () => {
    setLoading(true);
    const values = form.getFieldsValue();
    const objectCreateParking = {
      entryDate: values.entryDate.subtract(3, 'hours'),
      licensePlate: values.licensePlate,
      departureDate: values.departureDate.subtract(3, 'hours'),
      model: values.model,
      brand: values.brand,
      color: values.color,
    };
    await api
      .put('api/Parking', objectCreateParking)
      .then(() => {
        form.resetFields();
        message.success('Atualizado com sucesso!');
        setUpdateTable(!UpdateTable);
        setLoading(false);
        setOpenModal(false);
      })
      .catch((e) => {
        message.erro(e.message);
      });
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
        onFinish={UpdateParking ? onUpdateParking : onFinish}
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

          <Col span={24}>
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
              <DatePicker
                style={{ width: '100%' }}
                locale={locale}
                showTime
                format="DD/MM/YYYY HH:mm:ss"
              />
            </Form.Item>
          </Col>
        </Row>

        {UpdateParking && (
          <>
            <Col span={24}>
              <Form.Item
                label="DepartureDate"
                name="departureDate"
                rules={[
                  {
                    required: true,
                    message: 'Please input your departureDate!',
                  },
                ]}
              >
                <DatePicker showTime format="DD/MM/YYYY HH:mm:ss" />
              </Form.Item>
            </Col>

            <Button
              style={{ marginRight: 10 }}
              type="primary"
              onClick={() => deleteParking()}
            >
              Delete
            </Button>
          </>
        )}

        <Button type="primary" htmlType="submit">
          Send
        </Button>
      </Form>
    </Modal>
  );
}
