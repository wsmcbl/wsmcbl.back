using wsmcbl.back.model.dao;

namespace wsmcbl.back.controller.business;

public class BaseController(DaoFactory daoFactory)
{
    protected readonly DaoFactory daoFactory = daoFactory;
}