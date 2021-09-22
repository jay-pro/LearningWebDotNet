import React, { useState } from "react";
import { Table } from "antd";

function TableCourse({ columns, data, loading }) {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(3);

  return (
    <div>
      <h1 className="table-Course-title">List of Courses waiting approved</h1>
      <div className="table-Course">
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
          rowKey="idCourse"
        />
      </div>
    </div>
  );
}

export default TableCourse;
