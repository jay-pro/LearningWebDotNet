import React, { useState } from "react";
import { Table } from "antd";

function TableAssignment({ columns, data, loading }) {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(3);

  return (
    <div>
      <h1 className="table-Assignment-title">Assigments List</h1>
      <div className="table-Assignment">
        <Table
          columns={columns}
          dataSource={data}
          pagination={{
            current: page,
            pageSize: pageSize,
            onChange: (page, pageSize) => {
              setPage(page);
              setPageSize(pageSize);
            },
          }}
          loading={loading}
          rowKey="idAssignemnt"
        />
      </div>
    </div>
  );
}

export default TableAssignment;
