/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
/* eslint-disable import/no-extraneous-dependencies */
import React, { useEffect, useState } from 'react';
import {
  Modal,
  Form,
  Row,
  Input,
  DatePicker,
  Button,
  Col,
  InputNumber,
} from 'antd';
import dayjs from 'dayjs';
import locale from 'antd/es/date-picker/locale/pt_BR';
import { api } from '../../Services/HttpHandler';

export default function ModalCreateAndUpdatePrice({ open, setOpenModal }) {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const [price, setprice] = useState(false);

  const getPrice = async () => {
    await api
      .get('api/Price?id=1')
      .then(({ data }) => {
        setprice(true);
        form.setFieldsValue({
          ...data,
          initialDate: data?.initialDate ? dayjs(data?.initialDate) : '',
          finalDate: data?.finalDate ? dayjs(data?.finalDate) : '',
        });
      })
      .catch(({ response }) => {
        if (response.status === 500) {
          setprice(false);
          form.setFieldsValue();
        }
      });
  };

  const onFinish = async (values) => {
    setLoading(true);
    const objectCreateParking = {
      initialDate: values.initialDate.subtract(3, 'hours'),
      finalDate: values.finalDate.subtract(3, 'hours'),
      initialTime: values.initialTime,
      initialTimeValue: values.initialTimeValue,
      additionalHourlyValue: values.additionalHourlyValue,
    };
    await api
      .post('api/Price', objectCreateParking)
      .then(({ data }) => {
        form.setFieldsValue({
          ...data,
          initialDate: data?.initialDate ? dayjs(data?.initialDate) : '',
          finalDate: data?.finalDate ? dayjs(data?.finalDate) : '',
        });
        setLoading(false);
        setOpenModal(false);
      })
      .catch((e) => console.log(e));
  };

  const onUpdatePrice = async () => {
    setLoading(true);
    const values = form.getFieldsValue();
    const objectCreatePrice = {
      id: 1,
      initialDate: values.initialDate.subtract(3, 'hours'),
      finalDate: values.finalDate.subtract(3, 'hours'),
      initialTime: values.initialTime,
      initialTimeValue: values.initialTimeValue,
      additionalHourlyValue: values.additionalHourlyValue,
    };
    await api
      .put('api/Price', objectCreatePrice)
      .then(({ data }) => {
        form.setFieldsValue({
          ...data,
          initialDate: data?.initialDate ? dayjs(data?.initialDate) : '',
          finalDate: data?.finalDate ? dayjs(data?.finalDate) : '',
        });
        setLoading(false);
        setOpenModal(false);
      })
      .catch((e) => console.log(e));
  };

  useEffect(() => {
    getPrice();
  }, []);

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
        onFinish={price ? onUpdatePrice : onFinish}
        layout="vertical"
      >
        <Row gutter={[24]}>
          <Col span={24}>
            <Form.Item
              label="Data de inicio vigência"
              name="initialDate"
              rules={[
                {
                  required: true,
                  message: 'Informar a Data de inicio vigência é obrigatório!',
                },
              ]}
            >
              <DatePicker
                locale={locale}
                showTime
                format="DD-MM-YYYY HH:mm:ss"
              />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Data final vigência"
              name="finalDate"
              rules={[
                {
                  required: true,
                  message: 'Informar a Data final vigência é obrigatório!',
                },
              ]}
            >
              <DatePicker
                locale={locale}
                showTime
                format="DD-MM-YYYY HH:mm:ss"
              />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Hora Inicial (em minutos)"
              name="initialTime"
              rules={[
                {
                  required: true,
                  message: 'Informar a Hora Inicial é obrigatório!',
                },
              ]}
            >
              <InputNumber />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Valor da hora Inicial"
              name="initialTimeValue"
              rules={[
                {
                  required: true,
                  message: 'Informar a Hora Inicial é obrigatório!',
                },
              ]}
            >
              <InputNumber />
            </Form.Item>
          </Col>

          <Col span={24}>
            <Form.Item
              label="Valor da hora adicional"
              name="additionalHourlyValue"
              rules={[
                {
                  required: true,
                  message: 'Informar o Valor da hora adicional é obrigatório!',
                },
              ]}
            >
              <InputNumber />
            </Form.Item>
          </Col>
        </Row>

        <Button type="primary" htmlType="submit">
          Enviar
        </Button>
      </Form>
    </Modal>
  );
}
