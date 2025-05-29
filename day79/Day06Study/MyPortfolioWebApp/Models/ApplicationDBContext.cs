﻿using Microsoft.EntityFrameworkCore;

namespace MyPortfolioWebApp.Models
{
    /*DbContext : 직접 쿼리를 연결하고, 명령객체(MySqlCommand)로 처리하는 게 아니고
        일련의 CRUD 작업을 모두 래핑해서 사용할 수 있도록 만들어놓은 클래스 , 
        직접 쿼리를 작성할 필요 없음
     */
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDBContext()
        {
        }

        //DB와 연동할 모델폴더 내 클래스 선언
        public DbSet<News> news { get; set; }
        
    }
}
