#include "cmd.h"
#include "ui_cmd.h"

CMD::CMD(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::CMD)
{
    ui->setupUi(this);
}

CMD::~CMD()
{
    delete ui;
}

