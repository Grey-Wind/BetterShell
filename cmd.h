#ifndef CMD_H
#define CMD_H

#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class CMD; }
QT_END_NAMESPACE

class CMD : public QMainWindow
{
    Q_OBJECT

public:
    CMD(QWidget *parent = nullptr);
    ~CMD();

private:
    Ui::CMD *ui;
};
#endif // CMD_H
