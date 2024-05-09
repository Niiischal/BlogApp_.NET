import React, { useState } from "react";
import { BiUser } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import { Avatar, Badge, Dropdown } from "antd";
import { RiNotificationLine } from "react-icons/ri";
import { FaPlus } from "react-icons/fa";
import Blogform from "./Blogform";

function Navbar() {
  const navigate = useNavigate();
  const [showBlogForm, setShowBlogForm] = useState(false);
  const [showBlogModal, setShowBlogModal] = useState(false);
  return (
    <div>
      <div className='flex justify-between items-center pl-[2rem] pr-[2rem] bg-primary '>
        <div className='logo-div'>
          <h1 className='logo text-[27px] cursor-pointer text-white'>
            Bislerium
          </h1>
        </div>

        <div className='relative z-10 cursor-pointer rounded flex items-center gap-3 '>
          <FaPlus size={26} color='white' 
          onClick={() => {
            setShowBlogForm(true);
          }}/>
          <span className='text-white' onClick={() => navigate("/profile")}>
            {}
          </span>
          <Badge>
            <Avatar size='large' shape='square' icon={<RiNotificationLine />} />
          </Badge>
          <Dropdown trigger={["click"]}>
            <BiUser size={26} color='white' />
          </Dropdown>
        </div>
      </div>
      {showBlogForm && (
        <Blogform
        showBlogForm={showBlogForm}
        setShowBlogForm = {setShowBlogForm}
        />
      )}
    </div>
  );
}

export default Navbar;
