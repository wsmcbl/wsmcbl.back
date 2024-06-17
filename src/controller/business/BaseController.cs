using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class BaseController(DaoFactory daoFactory)
{
    protected readonly DaoFactory daoFactory = daoFactory;
}